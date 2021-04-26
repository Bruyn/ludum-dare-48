using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	//TODO - automaticly set owner
	[SerializeField] private GameObject owner;
	
    [SerializeField] private float shootCD = 1f;
	[SerializeField] private float damageAmount = 1f;
	[SerializeField] private int clipSize = 5;
	[SerializeField] private float reloadTime = .6f;
	[SerializeField] private int ammoPerReload = 5;
	[SerializeField] private Transform shootingPoing = null;
	[SerializeField] protected float inaccuracy = 5f;
	[SerializeField] private ParticleSystem muzzle;

	private Damage damage;
	private float currentCD = 0;
	private int currentClipSize;
	private bool reloading = false;
	private bool isOwnedByPlayer = false;

	protected virtual void Start()
	{
		currentClipSize = clipSize;
		damage = new Damage(owner, damageAmount);
		if (owner != null && owner.GetComponent<PlayerAttack>() != null)
			isOwnedByPlayer = true;
		HudController.Instance.SetAmmoCount(currentClipSize, clipSize);
	}
	
	// Method handles shooting cooldown, reloading and ammo wasting.
	// Returns true if the shot was successful.
	public virtual bool Shoot()
	{
		if (currentClipSize <= 0)
		{
			Reload();
			return false;
		}

		if (currentCD > Time.time)
			return false;
		if (reloading)
			StopReloading();

		currentCD = Time.time + shootCD;
		currentClipSize--;

		HudController.Instance.SetAmmoCount(currentClipSize, clipSize);
		SoundManager.Instance.PlayShotSound();
		muzzle.Play();
		return true;
	}

	protected virtual void StartProjectile(Quaternion rot)
	{
		if (rot == Quaternion.identity)
			rot = Quaternion.Euler(0, Random.Range(-inaccuracy, inaccuracy), 0);

		Projectile projectile = ProjectileController.Instance.PickProjectile();
		projectile.transform.position = shootingPoing.position;

		if (shootingPoing == null)
		{
			projectile.transform.rotation = transform.rotation * rot;
		}
		else
		{
			projectile.transform.rotation = shootingPoing.transform.rotation * rot;
		}
		projectile.SetUp(damage);
	}
	
	// Reload the gun.
	public void Reload()
	{
		if (reloading)
			return;

		//TODO start reload sound
		//Debug.Log("Starting reload");
		StartCoroutine(ReloadCoroutine());
	}

	private void StopReloading()
	{
		reloading = false;
		StopAllCoroutines();
	}

	private IEnumerator ReloadCoroutine()
	{
		reloading = true;
		while (currentClipSize < clipSize)
		{
			yield return new WaitForSeconds(reloadTime);
			//TODO insert ammo sound and hud
			currentClipSize += ammoPerReload;
		}
		reloading = false;
	}
}

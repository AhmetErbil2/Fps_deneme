using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shoot : MonoBehaviour
{
    public float coolDown = 10f;
    float lastFireTime = 0;
    private int LastAmmo;
    public int defaultAmmo = 120;
    public int magSize = 30;
    public int currentAmmo;
    public int currentMagAmmo;  
    public Camera Camera;
    public int range;
    [Header("Gun Damage On Hit")]
    public int damage;
    public ParticleSystem muzzleParticle;
    public GameObject MuzzleLight;
    public GameObject bloodPrefab;
    public GameObject decalPrefab;
    int minAngle = 0;
    int maxAngle = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = defaultAmmo-magSize;
        currentMagAmmo=magSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }




        if (Input.GetMouseButton(0))
        {
            if (CanFire())
            {
                Fire();
            }
             
        }
    }

    private void Reload()
    {
        if (currentAmmo == 0 && currentMagAmmo == magSize)
        {
            return;
        }
        if (currentAmmo < magSize)
        {
            currentMagAmmo =currentMagAmmo+ currentAmmo;
            currentAmmo = 0;

        }
        else
        {
            currentAmmo -= magSize - currentMagAmmo;
            currentMagAmmo = magSize;
        }
    }

    private bool CanFire()
    {

       

        if (currentMagAmmo > 0 && lastFireTime + coolDown < Time.time)
        {
            lastFireTime= Time.time + coolDown;
            return true;

        }
        
        return false;
        
    }

    private void Fire()
    {
        MuzzleLight.Equals(true);
        
        muzzleParticle.Play(true);
        currentMagAmmo -= 1;
        LastAmmo = currentAmmo + currentMagAmmo;
        Debug.Log("Toplam Mermi Sayýnýz" + LastAmmo);
        Debug.Log("Kalan Mermi Sayýnýz" + currentMagAmmo);

        

       RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position,Camera.transform.forward,out hit,100)) 
        {

            if (hit.transform.tag=="Zombie")
            {
                hit.transform.GetComponent<ZombieHealt>().hit(damage);
                GenerateBloodEffect(hit);
            }
               else
            {
                   GenerateHitEffect(hit);
               }

            transform.localEulerAngles = new Vector3(Random.Range(minAngle, maxAngle), Random.Range(minAngle, maxAngle), Random.Range(minAngle, maxAngle));



        }
    }


  private void GenerateHitEffect(RaycastHit hit)
   {
      // ///TODO: mermi izi oluþtur
       GameObject hitObject= Instantiate(decalPrefab, hit.point, Quaternion.identity);
        hitObject.transform.rotation = Quaternion.FromToRotation(decalPrefab.transform.forward, hit.normal);

    }

    private void GenerateBloodEffect(RaycastHit hit)
    {
        GameObject bloodObject =Instantiate(bloodPrefab,hit.point,hit.transform.rotation);
        
    }

    
}

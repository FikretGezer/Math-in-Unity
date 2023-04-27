using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class PlaneSlice : MonoBehaviour
{
    public Material _mat;
    public LayerMask _layer;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h, v, 0) * .5f;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Collider[] kesilecekNesneler = Physics.OverlapBox(transform.position, new Vector3(1f, .1f, .1f), transform.rotation, _layer);

            foreach (Collider nesne in kesilecekNesneler)
            {
                SlicedHull kesilenNesne = Kes(nesne.gameObject, _mat);
                GameObject kesilmisUst = kesilenNesne.CreateUpperHull(nesne.gameObject, _mat);
                GameObject kesilmisAlt = kesilenNesne.CreateLowerHull(nesne.gameObject, _mat);

                BilesenEkle(kesilmisUst);
                BilesenEkle(kesilmisAlt);
                Destroy(nesne.gameObject);
            }
        }
    }
    public SlicedHull Kes(GameObject obj, Material mat = null)
    {
        return obj.Slice(transform.position, transform.up, mat);
    }
    void BilesenEkle(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.AddComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        obj.AddComponent<Rigidbody>().AddExplosionForce(100, obj.transform.position, 20);
    }
}

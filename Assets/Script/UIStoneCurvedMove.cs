using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStoneCurvedMove : MonoBehaviour {

    private Transform bullet;   // 포물체
    private float tx;
    private float ty;
    private float tz;
    private float v;
    public float g = 9.8f;
    private float elapsed_time;
    public float max_height;
    private float t;
    private Vector3 start_pos;
    private Vector3 end_pos;
    private float dat;  //도착점 도달 시간 

    private void Start()
        {
        
        }

    private void Update()
        {
        
        }

    public void Shoot(Vector3 endPos, System.Action onComplete)
        {
        UnityEngine.Debug.Log("Shoot");
        start_pos = this.transform.localPosition;
        end_pos = endPos;
        this.g = 20f;
        this.max_height = 50f; // used atul 50
        this.bullet = this.transform;
        this.bullet.position = start_pos;
        var dh = endPos.y - start_pos.y;
        var mh = max_height - start_pos.y;    
//        ty = Mathf.Sqrt(2 * this.g * mh);
        ty = Mathf.Sqrt(2 * this.g);
        float a = this.g;
        float b = -2 * ty;
        float c = 2 * dh;
        dat = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
//        dat = 0.8f;
        tx = -(start_pos.x - endPos.x) / dat;
        tz = -(start_pos.z - endPos.z) / dat;
        this.elapsed_time = 0;
        StartCoroutine(this.ShootImpl(onComplete));
        }

    IEnumerator ShootImpl(System.Action onComplete)
        {
        UnityEngine.Debug.Log("ShootImpl");
        while (true)
            {
            this.elapsed_time += Time.deltaTime;
            var tx = start_pos.x + this.tx * elapsed_time;
            var ty = start_pos.y + this.ty * elapsed_time - 0.5f * g * elapsed_time * elapsed_time;
            var tz = start_pos.z + this.tz * elapsed_time;
            var tpos = new Vector3(tx, ty, tz);
//            bullet.transform.LookAt(tpos);
            bullet.transform.localPosition = tpos;
            if (this.elapsed_time >= this.dat)
                break;
            yield return null;
            }
        onComplete();
        }
}

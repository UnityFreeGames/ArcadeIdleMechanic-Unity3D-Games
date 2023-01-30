using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class workDesk : MonoBehaviour
{
    public Animator female_anim;
    [SerializeField] private Transform DollarPlace;
    [SerializeField] private GameObject Dollar;
    public float[] YAxis ;
    private IEnumerator makeMoneyIE;
    int counter = 0;
    int DollarPlaceIndex = 0;

    private void Start()
    {
        makeMoneyIE = MakeMoney();
        YAxis =new float[6];
        YAxis[0]=YAxis[1]=YAxis[2]=YAxis[3]=YAxis[4]=YAxis[5]=0f;
    }

    public void Work()
    {
        female_anim.SetBool("work",true);
        
        InvokeRepeating("DOSubmitPapers",2f,1f);

        StartCoroutine(makeMoneyIE);
    }

    private IEnumerator MakeMoney()
    {
            
        yield return new WaitForSecondsRealtime(2);

        while (counter < transform.childCount)
        {
            GameObject NewDollar = Instantiate(Dollar, new Vector3(DollarPlace.GetChild(DollarPlaceIndex).position.x,
                    YAxis[DollarPlaceIndex], DollarPlace.GetChild(DollarPlaceIndex).position.z),
                DollarPlace.GetChild(DollarPlaceIndex).rotation);
            
            NewDollar.transform.tag="D"+(DollarPlaceIndex+1);

            NewDollar.transform.DOScale(new Vector3(1.4f, 0.4f, 0.8f), 0.5f).SetEase(Ease.OutElastic);

            YAxis[DollarPlaceIndex] += 0.5f;

            if (DollarPlaceIndex < DollarPlace.childCount - 1)
            {
                DollarPlaceIndex++;             
            }
            else
            {
                DollarPlaceIndex = 0;            
            }
           
            
            
            yield return new WaitForSecondsRealtime(3f);
        }
    }

    void DOSubmitPapers()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject,1f);
        }
        else
        {
            female_anim.SetBool("work",false);

            var Desk = transform.parent;

            Desk.GetChild(Desk.childCount - 1).GetComponent<Renderer>().enabled = true;
            
            StopCoroutine(makeMoneyIE);
            makeMoneyIE = MakeMoney();

           // YAxis = 0f;
        }
    }
}

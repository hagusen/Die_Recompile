using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> ammoSliders;
    public List<GameObject> frames;
    public Text ammoCount;
    public Text maxAmmo;

    private int active;


    private void Start()
    {
        frames[0].SetActive(false);
        frames[1].SetActive(false);
        frames[2].SetActive(false);
        frames[3].SetActive(false);

        SetActive(AmmoType.Bullet);
    }

    public void SetAmmoText()
    {
        ammoCount.text = ammoSliders[active].GetComponent<Slider>().value.ToString() + " ";
        maxAmmo.text = "/ " +  ammoSliders[active].GetComponent<Slider>().maxValue.ToString();
    }

    public void SetMaxAmmo(AmmoType type, int value)
    {
        switch(type)
        {
            case AmmoType.Bullet:
                ammoSliders[0].GetComponent<Slider>().maxValue = value;
                ammoSliders[0].GetComponent<Slider>().value = value;
                break;

            case AmmoType.Shell:
                ammoSliders[1].GetComponent<Slider>().maxValue = value;
                ammoSliders[1].GetComponent<Slider>().value = value;
                break;

            case AmmoType.Explosive:
                ammoSliders[2].GetComponent<Slider>().maxValue = value;
                ammoSliders[2].GetComponent<Slider>().value = value;
                break;

            case AmmoType.Energy:
                ammoSliders[3].GetComponent<Slider>().maxValue = value;
                ammoSliders[3].GetComponent<Slider>().value = value;
                break;
        }

        
    }

    public void SetAmmo(AmmoType type, int value)
    {
        switch (type)
        {
            case AmmoType.Bullet:
                ammoSliders[0].GetComponent<Slider>().value = value;
                break;

            case AmmoType.Shell:
                ammoSliders[1].GetComponent<Slider>().value = value;
                break;

            case AmmoType.Explosive:
                ammoSliders[2].GetComponent<Slider>().value = value;
                break;

            case AmmoType.Energy:
                ammoSliders[3].GetComponent<Slider>().value = value;
                break;
        }
        SetAmmoText();
    }

    public void SetActive(AmmoType type)
    {
        frames[active].SetActive(false);

        switch (type)
        {
            case AmmoType.Bullet:
                frames[0].SetActive(true);
                active = 0;
                break;

            case AmmoType.Shell:
                frames[1].SetActive(true);
                active = 1;
                break;

            case AmmoType.Explosive:
                frames[2].SetActive(true);
                active = 2;
                break;

            case AmmoType.Energy:
                frames[3].SetActive(true);
                active = 3;
                break;
        }

        SetAmmoText();
    }
}
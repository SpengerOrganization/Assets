using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sword : Item
{
    public double attackDamage;
    public double attackSpeed;

    public Sword(int id, string name, Sprite icon, double attackDamage, double attackSpeed)
        : base(id, name, icon)
    {
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
    }


    public static Sword CreateSample(){
        return new Sword(0, "", null, 0, 0);
    }
}

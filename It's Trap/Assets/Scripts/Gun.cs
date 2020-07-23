﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Normal_Gun,
}



public class Gun : MonoBehaviour
{


    [Header("총 유형")]
    public WeaponType weaponType;

    [Header("총 연사속도 조")]
    public float fireRate;

    [Header("총알 개")]
    public int bulletCount;
    public int maxBulletCount;

    [Header("총구 섬광")]
    public ParticleSystem ps_MuzzleFlash;

    [Header("총알 프리팹")]
    public GameObject go_Bullet_Prefab;

    [Header("애니메이터")]
    public Animator animator;

    [Header("총알 스피드")]
    public float speed;


}

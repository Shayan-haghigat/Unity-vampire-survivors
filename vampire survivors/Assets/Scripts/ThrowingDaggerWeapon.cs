using System;
using UnityEngine;

public class ThrowingDaggerWeapon : MonoBehaviour
{
    [SerializeField] private float timeToAttack = 1f;
    [SerializeField] private GameObject knifePrefab;
    
    private float timer;
    private PlayerMove playerMove;

    private void Awake() 
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void Update() 
    {
        timer += Time.deltaTime;

        if (timer >= timeToAttack)
        {
            timer = 0;
            SpawnKnife();
        }
    }

    private void SpawnKnife()
    {
        if (knifePrefab == null || playerMove == null)
        {
            Debug.LogWarning("KnifePrefab or PlayerMove is not assigned.");
            return;
        }

        GameObject thrownKnife = Instantiate(knifePrefab);
        thrownKnife.transform.position = transform.position;

        // Assuming SetDirection takes two float arguments
        thrownKnife.GetComponent<ThrowingDaggerProjectile>().SetDirection(playerMove.lastHorizontalVector, 0f);
    }
}

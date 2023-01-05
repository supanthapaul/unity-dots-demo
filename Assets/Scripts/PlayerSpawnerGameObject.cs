using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerGameObject : MonoBehaviour {


    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int spawnAmount = 0;


    private void Start() {
        for (int i = 0; i < spawnAmount; i++) {
            GameObject playerGameObject = Instantiate(playerPrefab);
            MoveToPositionGameObject moveToPositionGameObject = playerGameObject.AddComponent<MoveToPositionGameObject>();
            moveToPositionGameObject.SetSpeed(Random.Range(1f, 5f));
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCreator : MonoBehaviour
{
    public List<ShadowBehaviour> levelShadows = new List<ShadowBehaviour>();
    public List<RoundRecordContainer> playerRecords = new List<RoundRecordContainer>();

    [SerializeField] private GameObject shadowPrefab;

    private void Start()
    {
        GameManager.Instance.levelShadowCreator = this;
    }
    public void CreateShadows()
    {
        foreach (var record in playerRecords)
        {
            var position = record.roundMovementRecords[0].position;

            var shadowBehaviour =
                Instantiate(shadowPrefab, position, Quaternion.identity).GetComponent<ShadowBehaviour>();
            
            shadowBehaviour.SetBehaviour(record);
            
            levelShadows.Add(shadowBehaviour);
        }
    }

    public void SetPlayerRecords(List<RoundRecordContainer> records)
    {
        playerRecords = records;
    }
    public void ErasePlayerRecords()
    {
        playerRecords = new List<RoundRecordContainer>();
    }
    public void ResetShadows()
    {
        foreach (var shadow in levelShadows)
        {
            if (shadow.affordanceInstance != null) Destroy(shadow.affordanceInstance.gameObject);
            Destroy(shadow.gameObject);
        }
        levelShadows.Clear();
    }
}

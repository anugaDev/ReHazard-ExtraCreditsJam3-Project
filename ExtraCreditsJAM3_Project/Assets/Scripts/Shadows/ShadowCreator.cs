using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCreator : MonoBehaviour
{
    public List<ShadowBehaviour> levelShadows = new List<ShadowBehaviour>();

    public List<RoundRecordContainer> playerRecords = new List<RoundRecordContainer>();


    [SerializeField] private GameObject shadowPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.levelShadowcreator = this;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void SetPlayerRecords(List<RoundRecordContainer> _records)
    {
        playerRecords = _records;
    }

    public void ResetShadows()
    {
        foreach (var shadow in levelShadows)
        {
            Destroy(shadow.gameObject);
        }
        levelShadows.Clear();
    }
}

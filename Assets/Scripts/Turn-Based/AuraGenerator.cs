using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraGenerator : MonoBehaviour
{
    public GameObject auraPow, auraDef, auraSpeed, auraEvade;
    public float auraHeight = 1.5f;

    private float auraTimer = 0.0f;
    private bool genPow, genDef, genSpeed, genEvade;
    private int numActive;
    private float auraInterval;

    // Start is called before the first frame update
    void Start()
    {
        numActive = 0;
        auraInterval = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (numActive > 0)
        {
            auraTimer += Time.deltaTime;
            if (numActive == 1)
                auraInterval = 0.3f;
            else if (numActive == 2)
                auraInterval = 0.15f;
            else if (numActive >= 3)
                auraInterval = 0.1f;

            if (auraTimer >= auraInterval)
            {
                int temp = Random.Range(0, numActive);
                GameObject[] activeAura = new GameObject[numActive];
                for (int i = 0; i < numActive; i++)
                {
                    if (genPow && i == 0)
                        activeAura[i] = auraPow;
                    else if (genDef && i == 0)
                        activeAura[i] = auraDef;
                    else if (genSpeed && i == 0)
                        activeAura[i] = auraSpeed;
                    else if (genEvade && i == 0)
                        activeAura[i] = auraEvade;

                    else if (genPow && i == 1 && activeAura[0] != auraPow)
                        activeAura[i] = auraPow;
                    else if (genDef && i == 1 && activeAura[0] != auraDef)
                        activeAura[i] = auraDef;
                    else if (genSpeed && i == 1 && activeAura[0] != auraSpeed)
                        activeAura[i] = auraSpeed;
                    else if (genEvade && i == 1 && activeAura[0] != auraEvade)
                        activeAura[i] = auraEvade;

                    else if (genPow && i == 2 && activeAura[0] != auraPow && activeAura[1] != auraPow)
                        activeAura[i] = auraPow;
                    else if (genDef && i == 2 && activeAura[0] != auraDef && activeAura[1] != auraDef)
                        activeAura[i] = auraDef;
                    else if (genSpeed && i == 2 && activeAura[0] != auraSpeed && activeAura[1] != auraSpeed)
                        activeAura[i] = auraSpeed;
                    else if (genEvade && i == 2 && activeAura[0] != auraEvade && activeAura[1] != auraEvade)
                        activeAura[i] = auraEvade;

                    else if (genPow && i == 3 && activeAura[0] != auraPow && activeAura[1] != auraPow && activeAura[2] != auraPow)
                        activeAura[i] = auraPow;
                    else if (genDef && i == 3 && activeAura[0] != auraDef && activeAura[1] != auraDef && activeAura[2] != auraDef)
                        activeAura[i] = auraDef;
                    else if (genSpeed && i == 3 && activeAura[0] != auraSpeed && activeAura[1] != auraSpeed && activeAura[2] != auraSpeed)
                        activeAura[i] = auraSpeed;
                    else if (genEvade && i == 3 && activeAura[0] != auraEvade && activeAura[1] != auraEvade && activeAura[2] != auraEvade)
                        activeAura[i] = auraEvade;
                }
                float offsetX = Random.Range(-0.75f, 0.75f);
                float offsetZ = Random.Range(-0.1f, 0.1f);
                //Debug.Log("Trying to spawn particle number " + temp);
                Instantiate(activeAura[temp], new Vector3(transform.position.x + offsetX, transform.position.y - 1.0f, transform.position.z + offsetZ), activeAura[temp].transform.rotation).GetComponent<BuffAura>().SetHeight(auraHeight);
                auraTimer = 0.0f;
            }
        }
    }

    public void StartGenerate(string type)
    {
        if (type == "pow" && !genPow)
        {
            genPow = true;
            numActive++;
        }
        else if (type == "def" && !genDef)
        {
            genDef = true;
            numActive++;
        }
        else if (type == "speed" && !genSpeed)
        {
            genSpeed = true;
            numActive++;
        }
        else if (type == "evade" && !genEvade)
        {
            genEvade = true;
            numActive++;
        }
    }

    public void StopGenerate(string type)
    {
        if (type == "pow" && genPow)
        {
            genPow = false;
            numActive--;
            auraTimer = 0.0f;
        }
        else if (type == "def" && genDef)
        {
            genDef = false;
            numActive--;
            auraTimer = 0.0f;
        }
        else if (type == "speed" && genSpeed)
        {
            genSpeed = false;
            numActive--;
            auraTimer = 0.0f;
        }
        else if (type == "evade" && genEvade)
        {
            genEvade = false;
            numActive--;
            auraTimer = 0.0f;
        }
        else if (type == "all")
        {
            genPow = false;
            genDef = false;
            genSpeed = false;
            genEvade = false;
            numActive = 0;
            auraTimer = 0.0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MinigameManagerScript : MonoBehaviour
{
    public GameObject _myPrefab;
    public GameObject _myWrongPrefab;

    public Material agoraVaiEssaDisgrassa;
    public int randomPastilhaID;

    // Start is called before the first frame update
    void Start()
    {
        float linha = 7f, coluna = 0f;
        int numberOfPads = 0;

        randomPastilhaID = Random.Range(0, 39);
        for (int fileiras = 0; fileiras < 8; fileiras++){
            coluna = -3.3f;
            for (int itens = 0; itens < 3; itens++){

                if (numberOfPads == randomPastilhaID){
                    GameObject _temp = Instantiate(_myWrongPrefab, new Vector3(coluna, linha, -0.001f), Quaternion.identity);
                    _temp.GetComponent<SpriteRenderer>().material = agoraVaiEssaDisgrassa;
                    _temp.name = "pastilha_" + numberOfPads;
                }
                else{
                    GameObject _temp = Instantiate(_myPrefab, new Vector3(coluna, linha, 0.001f), Quaternion.identity);
                    _temp.GetComponent<SpriteRenderer>().material = agoraVaiEssaDisgrassa;
                    _temp.name = "pastilha_" + numberOfPads;
                }
                    
                coluna = coluna + 3.3f;
                numberOfPads++;
            }

            coluna = -1.65f;
            linha = linha - 0.9526f;
            for (int itens = 0; itens < 2; itens++){
                if (numberOfPads == randomPastilhaID){
                    GameObject _temp = Instantiate(_myWrongPrefab, new Vector3(coluna, linha, -0.001f), Quaternion.identity);
                    _temp.GetComponent<SpriteRenderer>().material = agoraVaiEssaDisgrassa;
                    _temp.name = "pastilha_" + numberOfPads;
                }
                else{
                    GameObject _temp = Instantiate(_myPrefab, new Vector3(coluna, linha, 0.001f), Quaternion.identity);
                    _temp.GetComponent<SpriteRenderer>().material = agoraVaiEssaDisgrassa;
                    _temp.name = "pastilha_" + numberOfPads;
                }
                coluna = coluna + 3.3f;
                numberOfPads++;
            }
            linha = linha - 0.9526f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 50f)){
                if (hit.transform != null){
                    print(hit.transform.name);
                }
            }
        }
    }
}

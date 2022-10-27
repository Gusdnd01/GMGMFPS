using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class WeaponChoose : MonoBehaviour
{
    [SerializeField] private List<WeaponImageData> curWeaponsSO = new List<WeaponImageData>(); //SO들이 들어있는 리스트

    [SerializeField] private GameObject curButton = null; //무기 선택창의 현재 버튼의 정보 받아올 변수
        
    [SerializeField] private Image curWeapon = null; //현재 총을 받아올 이미지
    [SerializeField] private TextMeshProUGUI curMaxBullet = null; //현재 총의 최대 총아 수를 받아올 텍스트
    [SerializeField] private List<Image> curWeapons = new List<Image>(); //가장 최근에 선택한 종류별 총을 받아올 이미지 리스트
    [SerializeField] private List<string> curWeaponsBullet = new List<string>(); //가장 최근에 선택한 종류별 총을 받아올 이미지 리스트

    private string curKeyName = null; //현재 상호작용 된 키의 값을 받아올 변수
    [SerializeField] private List<string> curKeyNames = new List<string>(); //위의 변수와 작용하며 비교할 대충 string 리스트

    private void OnGUI() //Update와 비슷, UI를 사용할 때 대충 사용하면 댈듯
    {
        if(Event.current.keyCode != KeyCode.None) { //눌린 키가 키코드 None이 아닐 때 실행
            curKeyName = Event.current.keyCode.ToString(); //curKeyName 변수에 눌린 키의 string 값을 넣어준다
            KeyDownChange();
        }
    }
    public void CheckChoose() { //무기 선택창에서 무기를 선택했을 때 실행되는 메서드
        curButton = EventSystem.current.currentSelectedGameObject; //curButton 변수에 현재 상호작용 된 버튼의 정보를 받아온다.
        WeaponChange(); //현재
    }

    private void KeyDownChange() { //1 2 3 4 5 버튼을 이용해 최근 무기를 현재 무기로 변환하는 메서드
        for (int i = 0; i < curKeyNames.Count; i++) { //최근 총 종류의 개수 만큼 반복
            if (curKeyName == curKeyNames[i]) { //받아온 키의 이름과 최근 총 종류의 개수가 담긴 리스트의 값이 같다면
                curWeapon.sprite = curWeapons[i].sprite; //현재 총 이미지에 선택한 최근 총의 이미지를 넣어준다
                curMaxBullet.text = curWeaponsBullet[i]; //현재 총알 갯수에 최근 총의 총알 갯수를 넣어준다
            }
        }
    }

    private void WeaponChange() { //현재 총과 최근의 총을 저장하는 메서드
        for (int i = 0; i < curWeaponsSO.Count; i++) //SO가 들어있는 리스트의 길이 만큼 반복
        {
            if (curWeaponsSO[i].name == curButton.gameObject.name) //SO 이름과 curButton 변수의 이름을 비교한다
            {   
                curWeapon.sprite = curWeaponsSO[i].weaponImage; //curWeapon 변수의 이미지에 현재 상호작용 된 버튼과 같은 SO의 이미지를 넣는다
                curMaxBullet.text = curWeaponsSO[i].bulletNum.ToString(); //curMaxBullet 변수의 값에 현재 상호작용 된 버튼과 같은 SO의 값을 넣는다
                string curButtonParent = curButton.transform.parent.name; //tlqkf 변수에 현재 상호작용 된 버튼의 부모 이름을 받아온다

                //최근의 총을 저장
                for (int j = 0; j < curWeapons.Count; j++) { //curWeapons 리스트의 길이 만큼 반복 
                    if (curButtonParent == j.ToString()) { //curButtonParnet 변수의 값과 j가 같다면
                        curWeapons[j].sprite = curWeaponsSO[i].weaponImage; //고른 버튼의 이미지를 curWeapons 리스트 중 알맞는 인덱스의 값으로 넣는다
                        curWeaponsBullet[j] = curWeaponsSO[j].bulletNum.ToString(); //curWPBullet 변수에 최근 총들의 총알 갯수를 저장한다
                    }
                }
            }
        }
    }

    //버튼의 color 를 어둡게 바꾼다.
    //현재 무기 변수가 null이 아니라면 컬러를 원상복귀
}

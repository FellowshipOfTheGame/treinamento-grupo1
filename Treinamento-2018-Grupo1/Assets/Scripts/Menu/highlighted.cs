using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
 
public class highlighted : MonoBehaviour, ISelectHandler , IPointerEnterHandler{
    public int valor;
    public GameObject panell; 
    public string menuName;
    //modifica pos do menuPause caso de highlighted no botao
    public void OnPointerEnter(PointerEventData eventData){
        if(menuName == "MenuPause")panell.GetComponent<MenuPause>().mudarPos(valor);
        else if(menuName == "MenuOptions")panell.GetComponent<MenuOptions>().mudarPos(valor);
    }

    public void OnSelect(BaseEventData eventData){
    }
 }

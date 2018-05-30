using UnityEngine;

// Usado para conter as informações sobre uma determinada habilidade.
[CreateAssetMenu(fileName = "NewHability", menuName = "ScriptableObjects/Hability")]
public class Hability : ScriptableObject {

    // Nome usado para se referir a essa habilidade.
    public string _name = "newHability";

    // Efeito ao redor do player quando está com essa habilidade.
    public Sprite displayEffect;

    // Efeitos dessa habilidade na movimentação do player.
    [System.Serializable]
    public class MovimentationEffect {
        // Efeito deixado quando o player anda.
        public Sprite effect;
        // Modificador na velocidade do player.
        public float velocityModifier = 1.0f;
        // Modificador no deslizar do player.
        public float slideModifier = 1.0f;
        // Modificador no pulo do player.
        public float jumpModifier = 1.0f;
    } public MovimentationEffect movimentationEffect;


}

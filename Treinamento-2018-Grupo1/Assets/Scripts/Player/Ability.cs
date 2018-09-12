using UnityEngine;

// Usado para conter as informações sobre uma determinada habilidade.
[CreateAssetMenu(fileName = "NewAbility", menuName = "ScriptableObjects/Ability")]
public class Ability : ScriptableObject {

    // Nome usado para se referir a essa habilidade.
    public string _name = "newAbility";

    // Efeito ao redor do player quando está com essa habilidade.
    public Sprite displayEffect;

    // O nome do efeito sonoro dessa habilidade.
    public string soundEffectName;

    // Tipo da habilidade.
    public enum Type { Projectile }
    public Type type;

    // Guarda informações sobre possíveis projéteis gerados pela habilidade.
    [System.Serializable]
    public class ProjectileSettings {

        // Projétil que será utilizado com o tipo de habilidade apropriado.
        public GameObject _object;

        // Delay entre projéteis.
        public float delay = 0.35f;

    } public ProjectileSettings projectileSettings;

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

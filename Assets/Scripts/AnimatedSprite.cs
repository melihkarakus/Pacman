using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bu kod parçasý, AnimationSprite adýnda bir C# sýnýfý (class) tanýmlar.
// Bu sýnýf, sprite tabanlý animasyonlarý yönetmek için kullanýlýr.

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    // gameobject üzerindeki SpriteRenderer bileþenine eriþimi saðlar.
    public SpriteRenderer spriteRenderer { get; private set; }

    // Animasyonun kullanacaðý farklý sprite'larý içerir.
    public Sprite[] sprites;

    // Her bir animasyon karesinin ekran kalacaðý süreyi belirler.
    public float animationTime = 0.25f;

    // Animasyonun hangi karede olduðunu belirler ve sadece bu sýnýfta deðiþtirilebilir.
    public int animationFrame { get; private set; }

    // Animasyonun tekrarlanýp tekrarlanmayacaðýný belirler.
    public bool loop = true;

    // SpriteRenderer bileþenine eriþimi Awake() fonksiyonunda saðlar.
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Animasyonu baþlatmak için Start() fonksiyonunda InvokeRepeating() kullanýlýr.
    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    // Animasyonu bir sonraki kareye taþýmak için kullanýlýr.
    private void Advance()
    {
        // Eðer SpriteRenderer bileþeni devre dýþý býrakýlmýþsa iþlem yapma.
        if (!this.spriteRenderer.enabled)
        {
            return;
        }

        // Animasyon karesini bir sonraki kareye ilerlet.
        this.animationFrame++;

        // Eðer döngü etkinse ve animasyon sonuna gelinmiþse baþa sar.
        if (this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }

        // Eðer animasyon karesi geçerli bir aralýktaysa, sprite'i güncelle.
        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    // Animasyonu yeniden baþlatmak için kullanýlýr.
    public void Restart()
    {
        this.animationFrame = -1;
        Advance();
    }
}

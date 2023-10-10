using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bu kod par�as�, AnimationSprite ad�nda bir C# s�n�f� (class) tan�mlar.
// Bu s�n�f, sprite tabanl� animasyonlar� y�netmek i�in kullan�l�r.

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    // gameobject �zerindeki SpriteRenderer bile�enine eri�imi sa�lar.
    public SpriteRenderer spriteRenderer { get; private set; }

    // Animasyonun kullanaca�� farkl� sprite'lar� i�erir.
    public Sprite[] sprites;

    // Her bir animasyon karesinin ekran kalaca�� s�reyi belirler.
    public float animationTime = 0.25f;

    // Animasyonun hangi karede oldu�unu belirler ve sadece bu s�n�fta de�i�tirilebilir.
    public int animationFrame { get; private set; }

    // Animasyonun tekrarlan�p tekrarlanmayaca��n� belirler.
    public bool loop = true;

    // SpriteRenderer bile�enine eri�imi Awake() fonksiyonunda sa�lar.
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Animasyonu ba�latmak i�in Start() fonksiyonunda InvokeRepeating() kullan�l�r.
    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    // Animasyonu bir sonraki kareye ta��mak i�in kullan�l�r.
    private void Advance()
    {
        // E�er SpriteRenderer bile�eni devre d��� b�rak�lm��sa i�lem yapma.
        if (!this.spriteRenderer.enabled)
        {
            return;
        }

        // Animasyon karesini bir sonraki kareye ilerlet.
        this.animationFrame++;

        // E�er d�ng� etkinse ve animasyon sonuna gelinmi�se ba�a sar.
        if (this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }

        // E�er animasyon karesi ge�erli bir aral�ktaysa, sprite'i g�ncelle.
        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    // Animasyonu yeniden ba�latmak i�in kullan�l�r.
    public void Restart()
    {
        this.animationFrame = -1;
        Advance();
    }
}

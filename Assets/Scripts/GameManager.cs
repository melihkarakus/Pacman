using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Oyunda bulunan hayaletleri temsil eden dizi.
    public Ghost[] ghosts;
    // Oyundaki Pacman karakterini temsil eden deðiþken.
    public Pacman pacman;
    // Oyundaki yemleri (pellet) içeren transform nesnesi.
    public Transform pellets;
    // Hayalet puanlarý için çarpan deðeri.
    public int ghostMultiplier { get; private set; } = 1;
    // Oyuncunun skorunu tutan özellik.
    public int score { get; private set; }
    // Oyuncunun can sayýsýný tutan özellik.
    public int lives { get; private set; }

    private void Start() // Oyun baþladýðýnda çaðrýlan fonksiyon.
    {
        NewGame(); // Oyun baþladýðýnda yeni bir oyunu baþlatýr.
    }

    private void Update()
    {
        // Oyuncunun caný kalmadýðýnda ve herhangi bir tuþa basýldýðýnda...
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame(); // ...yeni bir oyun baþlatýlýr.
        }
    }

    private void NewGame()
    {
        SetScore(0); // Skor sýfýrlanýr.
        SetLives(3); // Can sayýsý 3 olarak ayarlanýr.
        NewRound(); // Yeni bir oyun bölümü baþlatýlýr.
    }

    private void NewRound() // Yeni bir oyun bölümünü baþlatan fonksiyon.
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true); // Yemler aktif hale getirilir.
        }
        ResetState(); // Oyun durumlarý sýfýrlanýr.
    }

    private void ResetState() // Oyun durumlarýný sýfýrlayan fonksiyon.
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState(); // Hayaletler tekrar aktif hale getirilir.
        }
        this.pacman.ResetState(); // Pacman karakteri aktif hale getirilir.
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false); // Hayaletler oyun bittiðinde devre dýþý býrakýlýr.
        }
        this.pacman.gameObject.SetActive(false); // Pacman karakteri oyun bittiðinde devre dýþý býrakýlýr.
    }

    private void SetScore(int score)
    {
        this.score = score; // Skor ayarlanýr.
    }

    private void SetLives(int lives)
    {
        this.lives = lives; // Can sayýsý ayarlanýr.
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points); // Oyuncu hayalet yediðinde puaný güncellenir.
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false); // Pacman karakteri öldüðünde devre dýþý býrakýlýr.

        SetLives(this.lives - 1); // Can sayýsý bir azaltýlýr.

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f); // Can sayýsý sýfýrdan büyükse, bir süre sonra oyun durumlarý sýfýrlanýr.
        }
        else
        {
            GameOver(); // Can sayýsý sýfýrsa oyun biter.
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.point);

        if (!HasRemainingPellet())
        {
            this.pacman.gameObject.SetActive(false);

            Invoke(nameof(NewRound), 3.0f); // Tüm pelletler yendiyse, yeni bir oyun bölümü baþlatýlýr.
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duraction);
        }

        PelletEaten(pellet);
        CancelInvoke(); // Invoke çaðrýlarýný iptal eder.
        Invoke(nameof(ResetGhostMultiplier), pellet.duraction); // GhostMultiplier'ý belirli bir süre sonra sýfýrlar.
    }

    private bool HasRemainingPellet()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1; // GhostMultiplier'ý sýfýrlar.
    }
}


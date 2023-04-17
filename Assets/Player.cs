//using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Events;

public class Player : MonoBehaviour
{ 
    [SerializeField] Character selectedCharacter;
    [SerializeField] Transform atkRef;
    [SerializeField] bool isBot;
    [SerializeField] List<Character> characterList;
    [SerializeField] UnityEvent onTakeDamage;
    
    public Character SelectedCharacter { get => selectedCharacter; }
    public List<Character> CharacterList { get => characterList;}


    private void Start()
    {
        if (isBot)
        {
            foreach(var character in characterList)
            {
                character.Button.interactable = false;
            }
        }
    }
    public void Prepare()
    {
        selectedCharacter = null ;
    }

    public void SelectCharacter(Character character)
    {
        selectedCharacter = character;
    }

    public void SetPlay(bool value)
    {
        if (isBot)
        {
            List<Character> lotterylist = new List<Character>();
            foreach (var character in characterList)
            {
                int ticket =Mathf.RoundToInt((float)character.CurrentHP / (float)character.MaxHP) * 10;
                for (int i = 0; i < ticket; i++)
                {
                    lotterylist.Add(character);
                }
            }
            int index = Random.Range(0,lotterylist.Count);
            selectedCharacter = lotterylist[index]; 
        }
        else
        {
            foreach (var character in characterList)
            {
                character.Button.interactable = value;
            }
        }
    }

    
    Vector3 direction = Vector3 .zero;
    public void Attack()
    {
        SelectedCharacter.transform.DOMove(atkRef.position, 1f);
    }
    
    public bool IsAttacking()
    {
        if (selectedCharacter == null)
            return false;

        return DOTween.IsTweening(selectedCharacter.transform);
    }

    public void TakeDamage(int damageValue)
    {
        selectedCharacter.ChangeHP(-damageValue);
        var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        spriteRend.DOColor(Color.red,0.5f).SetLoops(6, LoopType.Yoyo);
        onTakeDamage.Invoke();
  

    }
    public bool IsDamaging()
    {
        if (selectedCharacter == null)
            return false;

        var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        return DOTween.IsTweening(spriteRend); 
    }

    public void Remove(Character character)
    {
        if (characterList.Contains(character) == false)
            return;

        if (selectedCharacter == character)
            selectedCharacter = null;

        character.Button.interactable = false;
        character.gameObject.SetActive(false);
        characterList.Remove(character);
    }

    public void Return()
    {
        SelectedCharacter.transform.DOMove(SelectedCharacter.InitialPosition, 0.5f);
    }

    public bool IsReturning()
    {
        if (selectedCharacter == null)
            return false;

        return DOTween.IsTweening(selectedCharacter.transform);
    }
}

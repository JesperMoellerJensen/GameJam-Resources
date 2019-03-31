using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OxygenBankBehavior : EntityBehavior {

    public Transform OxygenBar;
    public float MaxCapacity = 500f;
    public float CurrentCapacity;

    private TextMesh _textMesh;
    private float _barPosYTop = 0.14f;
    private float _barPosYBot = -0.4528f;

    private float _barScaleYTop = 1.2f;
    private float _barScaleYBot = 0f;

    private void Start() {
        CurrentCapacity = MaxCapacity;
        _textMesh = GetComponentInChildren<TextMesh>();
    }

    private void Update() {
        UpdateOxygenBar();
        UpdateText();
    }
    public override ItemSlot Interact(ItemSlot selectedItemSlot) {

        //if(selectedItemSlot == null) {
        //    GiveOxygenToPlayer();
        //    return selectedItemSlot;
        //}

        if (selectedItemSlot != null && selectedItemSlot.Item.ID == ItemID.CarbonCrystal) {

            float temp = CurrentCapacity + 25f * selectedItemSlot.StackSize;

            if (temp > MaxCapacity) {
                CurrentCapacity = MaxCapacity;
                selectedItemSlot.StackSize = (int)((temp - MaxCapacity) / 25f);
                return selectedItemSlot;
            } else {
                CurrentCapacity = Mathf.Clamp(CurrentCapacity + 25f * selectedItemSlot.StackSize, 0, MaxCapacity);
                return null;
            }
        } else {
            GiveOxygenToPlayer();
        }
        return selectedItemSlot;
    }

    public void GiveOxygenToPlayer() {
        PlayerOxygenTank playerTank = FindObjectOfType<PlayerOxygenTank>();

        float oxygenNeeded = playerTank.MaxCapacity - playerTank.CurrentCapacity;

        if (CurrentCapacity > oxygenNeeded) {
            playerTank.CurrentCapacity = Mathf.Clamp(playerTank.CurrentCapacity + oxygenNeeded, 0, playerTank.MaxCapacity);
            CurrentCapacity -= Mathf.Clamp(oxygenNeeded, 0, MaxCapacity);
        } else {
            playerTank.CurrentCapacity = Mathf.Clamp(playerTank.CurrentCapacity + CurrentCapacity, 0, playerTank.MaxCapacity);
            CurrentCapacity = 0;
        }
    }

    private void UpdateOxygenBar() {

        float alpha = CurrentCapacity / MaxCapacity;
        float pos = Mathf.Lerp(_barPosYBot, _barPosYTop, alpha);
        float scale = Mathf.Lerp(_barScaleYBot, _barScaleYTop, alpha);

        OxygenBar.localPosition = new Vector3(0.5f, pos, 0);
        OxygenBar.localScale = new Vector3(1, scale, 1);
    }

    private void UpdateText() {
        _textMesh.text = $"{CurrentCapacity} / {MaxCapacity}";
    }


}

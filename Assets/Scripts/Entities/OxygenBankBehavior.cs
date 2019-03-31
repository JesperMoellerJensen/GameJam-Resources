using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OxygenBankBehavior : EntityBehavior {

    public Transform OxygenBar;
    public float MaxCapacity = 500f;
    public float CurrentCapacity;



    private float _barPosYTop = 0.14f;
    private float _barPosYBot = -0.4528f;

    private float _barScaleYTop = 1.2f;
    private float _barScaleYBot = 0f;

    private void Start() {
        CurrentCapacity = MaxCapacity;
    }

    private void Update() {
        UpdateOxygenBar();
    }
    public override void Interact(MouseInteract mouseInteract) {

        if(mouseInteract.SelectedItemSlot == null) {
            GiveOxygenToPlayer();
            return;
        }

        if (mouseInteract.SelectedItemSlot.Item.ID == ItemID.CarbonCrystal) {

            float temp = CurrentCapacity + 25f * mouseInteract.SelectedItemSlot.StackSize;

            if (temp > MaxCapacity) {
                CurrentCapacity = MaxCapacity;
                mouseInteract.SelectedItemSlot.StackSize = (int)((temp - MaxCapacity) / 25f);
            } else {
                CurrentCapacity = Mathf.Clamp(CurrentCapacity + 25f * mouseInteract.SelectedItemSlot.StackSize, 0, MaxCapacity);
                mouseInteract.SelectedItemSlot = null;
                mouseInteract.SelectedItemSlot.StackSize = 0;
            }
        } else {
            GiveOxygenToPlayer();
        }
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


}

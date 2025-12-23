using Member.PYH._Scripts.SO;
using UnityEngine;
using UnityEngine.UI;

public class BlackmarketInvenSlot : MonoBehaviour
{
    private ItemSO item;
    [SerializeField] private Image Logo;
    private Image image;

    private void Awake()
    {
        image = Logo.GetComponent<Image>();
    }
    
    public void SetSlot(ItemSO item)
    {
        
        this.item = item;
        Logo.sprite = item.Icon;
        image.color = new Color(255, 255, 255, 255);
    }
    public void ResetSlot()
    {
        item = null;
        Logo.sprite = null;
        image.color = new Color(255, 255, 255, 0);
    }

    public ItemSO GetSlotItem()
    {
        return item;
    }
}

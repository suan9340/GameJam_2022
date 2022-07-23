using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2Move : ItemMove
{
    private readonly WaitForSeconds itemDuring = new WaitForSeconds(10f);

    protected override void ItemEat()
    {
        ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.item2Eat, transform.position);

        AudioManager.Instance.Sound_ItemEat();
        AudioManager.Instance.ItemEatSound(true);

        StartCoroutine(DuringStar());
        
        gameObject.transform.position = new Vector3(100f, 100f, 0);
    }

    private IEnumerator DuringStar()
    {
        GameManager.Instance.SettingItemState(Player_Item_State.Staring);
        UIManager.Instance.ChangeAttackBar(true);

        playerData.current_attackPower = playerData.max_attackPower;

        yield return itemDuring;

        UIManager.Instance.ChangeAttackBar(false);

        Destroy(gameObject);
    }
}

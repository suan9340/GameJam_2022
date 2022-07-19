using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2Move : ItemMove
{
    private readonly WaitForSeconds itemDuring = new WaitForSeconds(5f);

    protected override void ItemEat()
    {
        gameObject.transform.position = new Vector3(10f, 10f, 0);

        AudioManager.Instance.ItemEat();
        ParticleManager.Instance.AddParticle(ParticleManager.ParticleType.item2Eat, transform.position);

        StartCoroutine(DuringStar());
    }

    private IEnumerator DuringStar()
    {
        GameManager.Instance.SettingItemState(Player_Item_State.Staring);
        UIManager.Instance.ChangeAttackBar(true);

        playerData.current_attackPower = playerData.max_attackPower;
        yield return itemDuring;

        GameManager.Instance.SettingItemState(Player_Item_State.Idle);
        UIManager.Instance.ChangeAttackBar(false);

        Destroy(gameObject);
    }
}

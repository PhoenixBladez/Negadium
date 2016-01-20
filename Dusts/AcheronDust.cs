public class AcheronDust : ModDust
{
    /* 
        Gorateron
        Made for: Acheron Yoyo (Charon)
    */
    public override void OnSpawn(Dust dust)
    {
        dust.noGravity = true;
    }

    public override bool Update(Dust dust)
    {
        if ((double)dust.scale > 0.5)
            dust.scale -= 0.03f;
        else
            dust.scale -= 0.05f;
        if ((double)dust.scale <= 0.5)
            dust.scale -= 0.05f;
        if ((double)dust.scale <= 0.25)
            dust.scale -= 0.05f;

        dust.velocity *= 0.94f;
        dust.scale -= (float)(1.0 / 325.0);
        float B = dust.scale;
        if ((double)B > 1.0)
            B = 1f;
        Lighting.AddLight((int)((double)dust.position.X / 16.0), (int)((double)dust.position.Y / 16.0), B * 0.6f, B * 0.2f, B);

        dust.rotation += dust.velocity.X / 3f;
        //dust.position += dust.velocity / 500;
        int oldAlpha = dust.alpha;
        dust.alpha = (int)(dust.alpha * 1.2);
        if (dust.alpha == oldAlpha)
        {
            dust.alpha++;
        }
        if (dust.alpha >= (125 / 0.98) + 5)
        {
            dust.alpha = (int)(125 / 0.98) + 5;
            dust.active = false;
        }
        return false;
    }
}
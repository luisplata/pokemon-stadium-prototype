namespace V2
{
    public class AbilitySpawner
    {
        private readonly AbilitiesFactory abilitiesFactory;

        public AbilitySpawner(AbilitiesFactory abilitiesFactory)
        {
            this.abilitiesFactory = abilitiesFactory;
        }
        
        // Logic

        public void SpawnPowerUp(string id)
        {
            abilitiesFactory.Create(id);
        }
    }
}
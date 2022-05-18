namespace repository.DAL
{
    public interface IClientSpecification
    {
        public bool IsSatisfiedBy(Client client);
    }
}
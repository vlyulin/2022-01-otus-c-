namespace repository.DAL
{
    /// <summary>
    /// Условия для отбора клиентов
    /// </summary>
    /// <remarks>
    /// шаблон проектирования "Спецификация"
    /// </remarks>
    public interface IClientSpecification
    {
        public bool IsSatisfiedBy(Client client);
    }
}
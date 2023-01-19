namespace Taxually.TechnicalTest.Core.Interfaces
{
    public interface IVatRequestProcessorFactory
    {
        public IVatRequestProcessor? GetVatRequestProcessor(string country);
    }
}

namespace MoodleSystem.Domain.Common.Model
{
    public class GetAllResponse<TValue> 
    {
        public IEnumerable<TValue> Values { get; init; }

        public GetAllResponse(IEnumerable<TValue> values)
        {
            Values = values;
        }

        public GetAllResponse()
        {

        }
    }
}

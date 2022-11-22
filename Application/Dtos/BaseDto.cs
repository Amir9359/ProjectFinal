using System.Collections.Generic;

namespace Application.Dtos
{
    public class BaseDto<T>
    {
        public BaseDto(T Data, List<string> Message, bool IsSucces)
        {
            this.Data = Data;
            this.Message = Message;
            this.IsSucces = IsSucces;
        }
        public T Data { get; private set; }
        public List<string> Message { get; private set; }
        public bool IsSucces { get; private set; }
    }
    public class BaseDto
    {
        public BaseDto( List<string> Message , bool IsSucces)
        {
            this.Message = Message;
            this.IsSucces = IsSucces;
        }
        public List<string> Message { get; private set; }
        public bool IsSucces { get; private set; }
    }
}
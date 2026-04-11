public interface IOperationStrategy {
    float Calculate(float value);
}

public class AddOperation : IOperationStrategy {
    readonly float value;
    
    public AddOperation(float value) {
        this.value = value;
    }
    
    public float Calculate(float value) => value + this.value;
}

public class MultiplyOperation : IOperationStrategy {
    readonly float value;
    
    public MultiplyOperation(float value) {
        this.value = value;
    }
    
    public float Calculate(float value) => value * this.value;
}
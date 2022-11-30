namespace HyVision.Tools
{
    /// <summary>
    /// 工具类编辑器接口
    /// </summary>
    /// <remarks>
    /// 这里没有加泛型约束，加了之后，编辑界面在 VS 中打开会报约束冲突，但可以编译，运行也没有问题
    /// </remarks>
    public interface IHyUserToolEdit<T>// where T : IHyUserTool
    {
        T Subject { get; set; }
    }
}

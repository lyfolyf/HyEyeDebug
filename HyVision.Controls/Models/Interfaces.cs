namespace HyVision.Models
{
    public interface IHyCloneable
    {
        /// <summary>
        /// 克隆工具
        /// </summary>
        /// <param name="containsData">是否包含 HyTerminal.Value</param>
        object Clone(bool containsData);
    }

    public interface IInternalResourcesDisposable
    {
        /// <summary>
        /// 释放内部资源
        /// </summary>
        void DisposeInternalResources();
    }
}

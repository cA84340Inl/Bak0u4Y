// 代码生成时间: 2025-09-22 23:36:40
using Microsoft.AspNetCore.Components;
using System;

// 组件类，用于生成随机数
public class RandomNumberGeneratorBlazorApp : ComponentBase
{
    // 随机数生成器
    private static readonly Random _random = new Random();

    [Parameter]
    public int? MaxValue { get; set; }

    [Parameter]
    public int? MinValue { get; set; }

    // 生成随机数
    private int GenerateRandomNumber()
    {
        // 检查MinMax参数是否设置
        if (MinValue == null || MaxValue == null || MinValue > MaxValue)
        {
            throw new ArgumentException("MinValue and MaxValue must be set and MinValue must be less than or equal to MaxValue.");
        }

        // 生成随机数
        return _random.Next((int)MinValue, (int)MaxValue + 1);
    }

    // 按钮点击事件，调用生成随机数的方法
    private void GenerateRandomNumberButtonClicked()
    {
        int randomNumber = GenerateRandomNumber();
        Console.WriteLine($"Random number generated: {randomNumber}");
        // 这里可以添加逻辑将随机数显示到UI上
    }

    // 渲染组件UI
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        // 设置组件参数
        await base.SetParametersAsync(parameters);

        // 检查参数是否有效
        if (MinValue.HasValue && MaxValue.HasValue)
        {
            if (MinValue.Value > MaxValue.Value)
            {
                throw new ArgumentException("MinValue must not be greater than MaxValue.");
            }
        }
    }
}

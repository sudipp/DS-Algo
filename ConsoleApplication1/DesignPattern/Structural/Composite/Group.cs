using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DesignPattern.Structural.Composite
{
    public class Group : IComponent
    {
        IList<IComponent> components = new List<IComponent>();
        public void AddShape(IComponent component)
        {
            components.Add(component);
        }
        public void Render()
        {
            foreach (var component in components)
                component.Render();
        }
        public void Resize()
        {
            foreach (var component in components)
                component.Resize();
        }
    }
}

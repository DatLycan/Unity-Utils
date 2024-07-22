namespace DatLycan.Packages.Utils {
    public interface IBuilder<out T> {
        public T Build();
    }
}

type Props<T extends readonly unknown[] | []> = {
  promises: T;
  children: (value: { -readonly [P in keyof T]: Awaited<T[P]> }) => JSX.Element;
};

const PromiseResolver = async <T extends readonly unknown[] | []>(
  props: Props<T>
) => {
  let response = await Promise.all(props.promises);

  return props.children(response);
};

export default PromiseResolver;

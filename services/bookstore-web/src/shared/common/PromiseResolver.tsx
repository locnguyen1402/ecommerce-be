type Props<T> = {
  promise: Promise<T>;
  children: (value: T) => JSX.Element;
};

const PromiseResolver = async <T extends any>(props: Props<T>) => {
  let response = await props.promise;

  return props.children(response);
};

export default PromiseResolver;

const getEnv = (): Environment => {
  if (typeof window !== "undefined") {
    return {
      apiUrl: process.env.NEXT_PUBLIC_API_URL || "",
    };
  }

  return {
    apiUrl: process.env.API_URL || "",
  };
};

export default getEnv;

type HTTPRequestMethod = "GET" | "POST" | "PUT" | "DELETE";

type RequestOptions = Omit<RequestInit, "method">;

function sendRequest<T>(
  method: HTTPRequestMethod,
  url: string,
  options?: RequestOptions
): Promise<SuccessResponse<T>> {
  const requestOptions: RequestInit = {
    method,
    ...options,
    headers: { "Content-Type": "application/json", ...options?.headers },
    cache: options?.cache || "no-store",
  };

  return fetch(url, requestOptions).then(handleResponse);
}

async function handleResponse(response: Response) {
  return response.text().then((text) => {
    const data = text && JSON.parse(text);

    if (!response.ok) {
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    let pagination: Nullable<PaginationInfo> = null;
    if (!!response.headers.get("X-Pagination")) {
      pagination = JSON.parse(response.headers.get("X-Pagination")!);
    }

    return {
      data,
      meta: {
        pagination,
      },
      statusCode: response.status,
    };
  });
}

export const HttpUtils = {
  get: <T>(url: string, options?: RequestOptions) =>
    sendRequest<T>("GET", url, options),
};

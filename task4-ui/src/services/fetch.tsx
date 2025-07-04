/* eslint-disable @typescript-eslint/no-explicit-any */
const host = "http://192.168.10.188:3000/api/v1/";

interface RequestOptions {
  method?: string;
  qs?: Record<string, string>;
  headers?: Record<string, string>;
  form?: boolean;
  body?: any;
}

const headers: Record<string, string> = {
  "User-Agent": "INTERVIEW_PROJECT/WEB/1.0.0",
  Referrer: host.replace("/api/", ""),
  Accept: "application/json",
  Device: "web",
};

function _fetch(uri: string, opts: RequestOptions = {}): Promise<any> {
  return new Promise(function (resolve, reject) {
    const origin = new URL(host + uri);
    for (const [key, value] of Object.entries(opts.qs || {})) {
      origin.searchParams.append(key, value);
    }
    opts.headers = { ...headers, ...opts.headers };
    if (
      opts.method === "PUT" ||
      opts.method === "POST" ||
      opts.method === "DELETE" ||
      opts.method === "PATCH"
    ) {
      if (!opts.form) opts.body = JSON.stringify(opts.body);
    }
    fetch(origin, opts)
      .then((response) => response.json())
      .then((res) => resolve(res))
      .catch(reject);
  });
}

export default _fetch;

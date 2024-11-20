import { servicesTools } from "@/services/Tools";

export interface FetchOptions {
    formData?: FormData;
    json?: Record<string, unknown>;
    method?: string;
    headers?: Record<string, string>;
}

export class apiClient {
    public static async FetchData<T>(
        url: string,
        options: FetchOptions = {}
    ): Promise<Response> {
        const method = options.method ?? (options.formData ? "POST" : options.json ? "POST" : "GET");
        const body = options.formData ?? JSON.stringify(options.json);
        const headers: Record<string, string> = { accept: "application/json", ...options.headers };
        if (options.json) {
            headers["content-type"] = "application/json";
        }
        if (options.formData) {
            delete headers["content-type"];
        }
        const r = await fetch(servicesTools.Tools.API_BACKEND_URL + url, {
            method,
            credentials: "include",
            body,
            headers,
        });
        return r;
    }
}
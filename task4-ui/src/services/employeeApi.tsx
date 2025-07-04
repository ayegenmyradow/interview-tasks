import Fetch from "./fetch"

/* eslint-disable @typescript-eslint/no-explicit-any */
export const getAllEmployees = (query:any) => {
    const params = new URLSearchParams();
    for (const [key, value] of Object.entries(query)) {
        params.set(key, value as string);
    }
    return Fetch('Employees?' + params.toString());
}
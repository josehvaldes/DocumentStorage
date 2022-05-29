import { authAtom } from '../_state';
import { useRecoilState } from 'recoil';

export { useFetchWrapper };

function useFetchWrapper() {
    const [auth, setAuth] = useRecoilState(authAtom);

    return {
        get: request('GET'),
        post: request('POST'),
        put: request('PUT'),
        delete: request('DELETE'),
        upload: uploadRequest('POST'),
        download: download('GET'),
    };

    function download(method) {
        return (url, name) => {
            const requestOptions = {
                method,
                headers: authHeader(url),
            };

            requestOptions.headers['Content-Type'] = 'application/json';

            return fetch(url, requestOptions).then(result => result.blob())
                .then(blob => {
                    const url = window.URL.createObjectURL(blob);
                    const a = document.createElement("a");
                    a.style.display = "none";
                    a.href = url;
                    a.download = name;
                    document.body.appendChild(a);
                    a.click();
                    window.URL.revokeObjectURL(url);
                    alert("File has downloaded!");
                }).catch(handleError);
        }
    }

    function uploadRequest(method) {
        return (url, body) => {
            const requestOptions = {
                method,
                headers: authHeader(url),
            };
            const formData = new FormData();
            formData.append('FormFile', body.files[0]); // only the first file
            formData.append('name', body.name);
            formData.append('category', body.category);
            formData.append('description', body.description);

            requestOptions.body = formData;

            return fetch(url, requestOptions).then(handleResponse).catch(handleError);
        };
    }

    function request(method) {
        return (url, body) => {
            const requestOptions = {
                method,
                headers: authHeader(url),
            };

            if (body) {
                requestOptions.headers['Content-Type'] = 'application/json';
                requestOptions.body = JSON.stringify(body);
            }

            return fetch(url, requestOptions).then(handleResponse).catch(handleError);
        }
    }

    // helper functions

    function authHeader(url) {
        // return auth header with jwt if user is logged in and request is to the api url
        var token = auth?.token;
        const isLoggedIn = !!token;
        const isApiUrl = url.startsWith(process.env.REACT_APP_API_URL);
        if (isLoggedIn && isApiUrl) {
            return { Authorization: `Bearer ${token}` };
        } else {
            return {};
        }
    }

    function handleError(error)
    {
        //just log the error
        console.log(error);
    }

    function handleResponse(response) {
        return response.text().then(text => {
            const data = text && JSON.parse(text);

            if (!response.ok) {
                if ([401, 403].includes(response.status) && auth?.token) {
                    // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                    localStorage.removeItem('user');
                    setAuth(null);
                    //history.push('/login');
                }
                //[405] Not allowed
                const error = (data && data.message) || response.statusText;
                return Promise.reject(error);
            }

            return data;
        });
    }
}

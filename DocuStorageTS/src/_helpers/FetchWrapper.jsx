

const baseGroupUrl = `${process.env.REACT_APP_API_URL}/groups`;
const baseDocumentsUrl = `${process.env.REACT_APP_API_URL}/documents`;
const baseCategoriesUrl = `${process.env.REACT_APP_API_URL}/categories`;
const baseBackupDocumentsUrl = `${process.env.REACT_APP_API_URL}/backups`;

export function FetchBackupDocuments(auth, category) {
    var token = auth?.token;
    const requestOptions = {
        method: 'GET',
        headers: authHeader(baseGroupUrl, token),
    };

    var url = baseBackupDocumentsUrl + "/" + category;

    if (token)
        return fetch(url, requestOptions).then(handleResponse).catch(handleError);
    else 
        return Promise.resolve();
}

export function FetchCategories(auth) {
    var token = auth?.token;
    const requestOptions = {
        method: 'GET',
        headers: authHeader(baseGroupUrl, token),
    };

    if (token)
        return fetch(baseCategoriesUrl, requestOptions).then(handleResponse).catch(handleError);
    else
        return Promise.resolve();
}


export function FetchDocuments(auth) {
    var token = auth?.token;
    const requestOptions = {
        method: 'GET',
        headers: authHeader(baseGroupUrl, token),
    };
    if (token)
        return fetch(baseDocumentsUrl, requestOptions).then(handleResponse).catch(handleError);
    else
        return Promise.resolve();
}

export function FetchGroups(auth) {
    var token = auth?.token;
    const requestOptions = {
        method: 'GET',
        headers: authHeader(baseGroupUrl, token),
    };
    if (token)
        return fetch(baseGroupUrl, requestOptions).then(handleResponse).catch(handleError);
    else
        return Promise.resolve();
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);

        if (!response.ok) {
            if ([401, 403].includes(response.status) /*&& auth?.token*/) {
                // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                localStorage.removeItem('user');
                console.log("LOGOUT!");
                alert("Unauthorized or Forbidden access. Logout and try again");
                //instance.logoutPopup().catch(e => {
                //    console.error(e);
                //});
            }
            //[405] Not allowed
            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}

function handleError(error) {
    //just log the error
    console.log("Fetch failed");
    console.log(error);
}


function authHeader(url, token) {
    // return auth header with jwt if user is logged in and request is to the api url
    const isLoggedIn = !!token;
    const isApiUrl = url.startsWith(process.env.REACT_APP_API_URL);
    if (isLoggedIn && isApiUrl) {
        return { Authorization: `Bearer ${token}` };
    } else {
        return {};
    }
}




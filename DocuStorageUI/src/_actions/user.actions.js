import * as React from 'react';
import { useSetRecoilState } from 'recoil';

import { history, useFetchWrapper } from '_helpers';
import { authAtom, usersAtom, documentsAtom, groupsAtom, documentsByUserAtom, documentsByGroupAtom, documentsInGroupsAtom } from '_state';

export { useUserActions };

function useUserActions () {
    const baseUrl = `${process.env.REACT_APP_API_URL}/users`;
    const baseDocUrl = `${process.env.REACT_APP_API_URL}/documents`;
    const baseGroupUrl = `${process.env.REACT_APP_API_URL}/groups`;

    const fetchWrapper = useFetchWrapper();
    const setAuth = useSetRecoilState(authAtom);
    const setUsers = useSetRecoilState(usersAtom);
    const setDocuments = useSetRecoilState(documentsAtom);
    const setDocumentsByUser = useSetRecoilState(documentsByUserAtom);
    const setDocumentsByGroup = useSetRecoilState(documentsByGroupAtom);
    const setGroups = useSetRecoilState(groupsAtom);
    const setDocumentsInGroups = useSetRecoilState(documentsInGroupsAtom);

    return {
        login,
        logout,
        getAll,
        createUser,
        uploadDocument,
        getAllDocuments,
        createGroup,
        getAllGroups,
        getGroupsByUser,
        setGroupsUser,
        getDocumentsByUser,
        setUserDocuments,
        setGroupDocuments,
        getDocumentsByGroup,
        getDocumentsInGroupsByUser,
        downloadDocument,
        deleteDocument
    }

    function deleteDocument(documentId)
    {
        return fetchWrapper.delete(`${baseDocUrl}/${documentId}`);
    }

    function downloadDocument(documentId, name)
    {
        return fetchWrapper.download(`${baseDocUrl}/download/${documentId}/${name}`, name);
    }

    function getDocumentsInGroupsByUser(userid)
    {
        return fetchWrapper.get(`${baseDocUrl}/ingroupsbyuser/${userid}`).then(setDocumentsInGroups);
    }

    function setGroupsUser(params)
    {
        return fetchWrapper.post(`${baseGroupUrl}/assigntouser/${params.userId}`, params.groups).then(response => {
            history.push('/success');
            return response;
        });
    }


    function getDocumentsByGroup(groupid)
    {
        return fetchWrapper.get(`${baseDocUrl}/group/${groupid}`).then(result => {
            setDocumentsByGroup(result);
            return result;
        });
    }

    function setGroupDocuments(params) {
        return fetchWrapper.post(`${baseDocUrl}/assigntogroup/${params.groupId}`, params.documents).then(response => {
            history.push('/success');
            return response;
        });
    }

    function setUserDocuments(params)
    {
        return fetchWrapper.post(`${baseDocUrl}/assigntouser/${params.userId}`, params.documents).then(response => {
            history.push('/success');
            return response;
        });
    }

    function getDocumentsByUser(userId)
    {
        return fetchWrapper.get(`${baseDocUrl}/user/${userId}`).then(result => {
            setDocumentsByUser(result);
            return result;
        });
    }

    function getGroupsByUser(userId)
    {
        return fetchWrapper.get(`${baseGroupUrl}/user/${userId}`);
    }

    function getAllGroups() {
        return fetchWrapper.get(baseGroupUrl).then(setGroups);
    }

    function createGroup(group)
    {
        return fetchWrapper.post(`${baseGroupUrl}/create`, group).then(response => {
            history.push('/success');
            return response;
        });
    }

    function getAllDocuments()
    {
        return fetchWrapper.get(baseDocUrl).then(setDocuments);
    }

    function uploadDocument(document)
    {
        return fetchWrapper.upload(`${baseDocUrl}/upload`, document).then(response => {
            history.push('/success');
            return response;
        });
    }


    function createUser(user)
    {
        return fetchWrapper.post(`${baseUrl}/create`, user).then(response => {
            history.push('/success');
            return response;
        });
    }

    function login(username, password) {

        return fetchWrapper.post(`${baseUrl}/authenticate`, { username, password })
            .then(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
                setAuth(user);

                // get return url from location state or default to home page
                const { from } = history.location.state || { from: { pathname: '/' } };
                history.push(from);
            });
    }

    function logout() {
        // remove user from local storage, set auth state to null and redirect to login page
        localStorage.removeItem('user');
        setAuth(null);
        history.push('/login');
    }

    function getAll() {
        return fetchWrapper.get(baseUrl).then(setUsers);
    }    
}

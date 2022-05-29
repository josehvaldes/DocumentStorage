import React, { useEffect, useState } from "react";
import { useFetchWrapper } from '../_helpers';
import { useRecoilState } from 'recoil';
import { groupsAtom, documentsAtom } from '../_state';

export { useUserActions };

function useUserActions() {

    const baseGroupUrl = `${process.env.REACT_APP_API_URL}/groups`;
    const baseDocumentsUrl = `${process.env.REACT_APP_API_URL}/documents`;

    const fetchWrapper = useFetchWrapper();
    const [groups, setGroups] = useRecoilState(groupsAtom);
    const [documents, setDocuments] = useRecoilState(documentsAtom);

    return {
        getAllGroups,
        getAllDocuments,
    }

    function getAllGroups() {
        
        return fetchWrapper.get(baseGroupUrl).then((response) =>
        {
            setGroups(response);
        });
    }

    function getAllDocuments() {
        return fetchWrapper.get(baseDocumentsUrl).then((response) => {
            console.log(response);
            setDocuments(response);
        });
    }
}
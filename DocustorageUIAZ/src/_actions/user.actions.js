import React, { useEffect, useState } from "react";
import { useFetchWrapper } from '../_helpers';
import { useRecoilState } from 'recoil';
import { groupsAtom, documentsAtom, categoriesAtom, backupdocumentsAtom } from '../_state';

export { useUserActions };

function useUserActions() {

    const baseGroupUrl = `${process.env.REACT_APP_API_URL}/groups`;
    const baseDocumentsUrl = `${process.env.REACT_APP_API_URL}/documents`;
    const baseCategoriesUrl = `${process.env.REACT_APP_API_URL}/categories`;
    const baseBackupDocumentsUrl = `${process.env.REACT_APP_API_URL}/backups`;

    const fetchWrapper = useFetchWrapper();
    const [groups, setGroups] = useRecoilState(groupsAtom);
    const [documents, setDocuments] = useRecoilState(documentsAtom);
    const [categories, setCategories] = useRecoilState(categoriesAtom);
    const [backupDocuments, setBackupDocuments] = useRecoilState(backupdocumentsAtom);

    return {
        getAllGroups,
        getAllDocuments,
        getCategories,
        getBackupDocuments,
    }

    function getBackupDocuments(category)
    {
        var url = baseBackupDocumentsUrl + "/" + category;
        console.log("the url: "+url)
        return fetchWrapper.get(url).then((response) =>
        {
            setBackupDocuments(response);
        });
    }


    function getAllGroups() {
        
        return fetchWrapper.get(baseGroupUrl).then((response) =>
        {
            setGroups(response);
        });
    }

    function getAllDocuments() {
        return fetchWrapper.get(baseDocumentsUrl).then((response) => {
            setDocuments(response);
        });
    }

    function getCategories()
    {
        return fetchWrapper.get(baseCategoriesUrl).then((response) => {
            setCategories(response);
        });
    }
}
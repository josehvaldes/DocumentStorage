import React from "react";
import { LeftNav } from './LeftNav';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Success, Home, Groups, Documents, BackupDocuments } from '../components';
import { PrivateRoute } from './PrivateRoute';

export const ContentBody = () => {

    return (
        <div className="bg-light">
            <BrowserRouter>
                <LeftNav />
                <div className="bodycontainer pt-4 pb-4">
                    <Routes>
                        <Route path="/" element={
                            <PrivateRoute><Home /></PrivateRoute >
                        } />
                        <Route path="/" element={<Home />} />
                        <Route path="/groups" element={<Groups />} />
                        <Route path="/documents" element={<Documents />} />
                        <Route path="/backupdocuments" element={<BackupDocuments />} />
                        <Route path="/success" element={<Success />} />
                    </Routes>
                </div>
            </BrowserRouter>
        </div>
    );

};
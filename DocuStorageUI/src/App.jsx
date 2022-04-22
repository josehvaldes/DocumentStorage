import { Router, Route, Switch, Redirect, Link } from 'react-router-dom';

import { Nav, LeftNav, PrivateRoute } from '_components';
import { history } from '_helpers';
import { Home } from 'home';
import { Login } from 'login';
import {
    EditUser, UploadDocument, Success, ListDocuments, EditGroup,
    AssignDocsUsers, AssignDocsGroups, AssignUserGroup, DeleteDocuments
} from 'Views'
export { App };

function App() {
    return (
        <div className="app-container bg-light">
            <Router history={history}>
                <Nav />
                <LeftNav />
                <div className="container pt-4 pb-4">
                    <Switch>
                        <PrivateRoute exact path="/" component={Home} />
                        <Route path="/login" component={Login} />
                        <Route path="/AddUser" component={EditUser} />
                        <Route path="/UploadDocument" component={UploadDocument} />
                        <Route path="/ListDocuments" component={ListDocuments} />
                        <Route path="/AddGroup" component={EditGroup} />
                        <Route path="/AssignDocsUsers" component={AssignDocsUsers} />
                        <Route path="/AssignDocsGroups" component={AssignDocsGroups} />
                        <Route path="/AssignUserGroup" component={AssignUserGroup} />
                        <Route path="/DeleteDocuments" component={DeleteDocuments} />
                        <Route path="/Success" component={Success} />
                        <Redirect from="*" to="/" />
                    </Switch>
                </div>
            </Router>
        </div>
    );
}

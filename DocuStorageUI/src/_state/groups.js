import { atom } from 'recoil';

const groupsAtom = atom({
    key: 'groups',
    default: null
});

export { groupsAtom };
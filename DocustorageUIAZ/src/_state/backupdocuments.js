import { atom } from 'recoil';

const backupdocumentsAtom = atom({
    key: 'backupdocuments',
    default: null,
    writable: true
});

export { backupdocumentsAtom };
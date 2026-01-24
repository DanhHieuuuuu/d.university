'use client';

import React from 'react';

const ChatbotLayout = ({ children }: { children: React.ReactNode }) => {
  return <div style={{ minHeight: '100vh' }}>{children}</div>;
};

export default ChatbotLayout;
